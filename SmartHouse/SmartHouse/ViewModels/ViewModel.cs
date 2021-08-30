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
                e.Apply(e.Target);
            }
            DirtyModels.Clear();
        }

        public static object CreateModel(object target)
        {
            Type t = target.GetType();
            var tn = t.Name;
            Type rt = Type.GetType("SmartHouse.ViewModels." + tn + "Model");
            var m = Activator.CreateInstance(rt) as ViewModel;
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

        private bool isdeleted = false;
        public bool IsDeleted
        {
            get => isdeleted;
            set
            {
                if (value != isdeleted)
                {
                    isdeleted = value;
                    IsDirty = true;
                }
            }
        }


        private bool isDirty = false;
        public bool IsDirty
        {
            get
            {
                return isDirty;
            }

            set
            {
                OnPropertyChanging("IsDirty");
                isDirty = value;
                if (value)
                    OnIsDirty?.Invoke();
                OnPropertyChanged("IsDirty");
            }
        }

        public Boolean IsAdmin { get { return SmartHouse.Models.Settings.Instance.IsAdmin; } }
        public Boolean NotIsAdmin { get { return !IsAdmin; } }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
        }

        public virtual void Setup(params object[] args)
        {

        }

        public virtual void Assign(ViewModel source)
        {
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
