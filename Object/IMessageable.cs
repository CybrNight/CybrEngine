namespace CybrEngine {
    public interface IMessageable {

        public abstract void SendMessage(string name, object[] args = null);

    }
}
