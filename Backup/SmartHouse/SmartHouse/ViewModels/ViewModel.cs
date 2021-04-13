using System.ComponentModel;
using System;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace SmartHouse.ViewModels
{
    public class ViewModel : BindableObject, INotifyPropertyChanged
    {
        public static event ParameterlessDelegate OnIsDirty;
        public static Dictionary<ViewModel, ViewModel> DirtyModels { get; set; } = new Dictionary<ViewModel, ViewModel>();

        public static void ApplyAllDirty()
        {
            foreach(var e in DirtyModels.Values)
            {
                e.Apply(e.target);
            }
            DirtyModels.Clear();
        }

        public static object CreateModel(object target)
        {
            Type t = target.GetType();
            var tn = t.Name;
            Type rt = Type.GetType("SmartHouse.ViewModels." + tn + "Model");
            var m = Activator.CreateInstance(rt) as ViewModel;
            m.target = target;
            m.Setup(t);
            return m;
        }

        public delegate void ParameterlessDelegate();
        protected virtual object CheckIsDirty(object oldValue, object newValue, string eventName, ParameterlessDelegate setter)
        {

            if (Object.Equals(oldValue, newValue))
                return oldValue;
            else
            {
                setter?.Invoke();
                if (!IsDirty)
                {
                    IsDirty = true;
                    if (!DirtyModels.ContainsKey(this))
                        DirtyModels.Add(this, this);
                }
                OnPropertyChanged(eventName);
                return newValue;
            }
        }

        public ViewModel Parent { get; set; } = null;

        private bool isDirty = false;
        public bool IsDirty
        {
            get
            {
                return isDirty;
                // return (Project)GetValue(SelectedItemProperty);
            }

            set
            {
                // OnPropertyChanging("IsDirty");
                isDirty = value;
                if (value)
                    OnIsDirty?.Invoke();
                OnPropertyChanged("IsDirty");
            }
        }

        public virtual void SetDirty(bool value)
        {
            isDirty = true;
        }


        public Boolean IsAdmin { get { return SmartHouse.Models.Settings.Instance.IsAdmin; } }
        public Boolean NotIsAdmin { get { return !IsAdmin; } }

        private object target;
        public object Target
        {
            get
            {
                return target;
            }
            set
            {
                OnPropertyChanging("Target");
                target = value;
                OnPropertyChanged("Target");
            }
        }

        /* public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected void OnPropertyChanging(string name)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(name));
        } */


        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
        }


        public virtual void Setup(params object[] args)
        {

        }

        public virtual void Assign(ViewModel source)
        {
            this.target = source.target;
        }

        public virtual void Apply(object target)
        {
            IsDirty = false;
        }

        public virtual ViewModel Clone()
        {
            
            throw new Exception("Not implemented");
        }

        public ViewModel()
        {

        }


    }
}
