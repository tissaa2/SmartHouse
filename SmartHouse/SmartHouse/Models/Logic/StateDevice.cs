using Newtonsoft.Json;

namespace SmartHouse.Models.Logic
{
    public class StateDevice<StateType> : Device
    {
        
        private StateType state;
        [JsonProperty(PropertyName = "State")]
        public StateType State
        {
            get { return state; }
            set { this.state = value; OnPropertyChanged("State"); }
        }


        public StateDevice()
        {

        }

        public StateDevice(string name, StateType state): base (UIDID.NewID(), name, null)
        {
            State = state;
        }

        public override BaseEntity<UID> Clone()
        {
            return new StateDevice<StateType>() { ID = ID, Icon = Icon, Name = Name, SecurityLevel = SecurityLevel, State = state };
        }


    }
}
