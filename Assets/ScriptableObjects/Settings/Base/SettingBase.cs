using System;
using UniRx;
using UnityEngine;

namespace ScriptableObjects.Settings.Base
{
	public abstract class SettingBase<T> : SettingBase, Interfaces.ISavedSetting<T>, IDisposable
	{
		protected abstract T DefaultValue { get; }
    	
		protected string Key => $"{GetType().Name}Setting";
    	
		public abstract ReactiveProperty<T> Setting { get; }
    	
		public abstract void Save();

		public void Dispose()
		{
			Setting.Dispose();
		}
	}

	public abstract class SettingBase : ScriptableObject, Interfaces.ILoadedSetting
	{
		public abstract void Load();
	}
}
