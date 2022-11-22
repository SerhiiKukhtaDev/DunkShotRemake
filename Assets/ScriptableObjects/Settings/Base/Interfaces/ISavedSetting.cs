namespace ScriptableObjects.Settings.Base.Interfaces
{
    public interface ISavedSetting<TType> : ISetting<TType>
    {
        void Save();
    }
}