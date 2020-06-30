using FestiSpec.SharedCode.Repositories;
using GalaSoft.MvvmLight;
using SharedCode.Models;
using System;

namespace Festispec.DesktopApplication.ViewModels
{
    public class EventInspectionViewModel : ViewModelBase
	{
		#region Model Refrences
		private EventInspection _eventInspection;
		#endregion

		#region Getters & Setters

		public int Id
		{
			get => this._eventInspection.Id;
		}
		public int EventId
		{
			get => this._eventInspection.EventId;
			set
			{
				this._eventInspection.EventId = value;
			}
		}
		public string Name
		{
			get => this._eventInspection.Name;
			set
			{
				this._eventInspection.Name = value;
			}
		}
		
		public DateTime CreatedAt
		{
			get => this._eventInspection.CreatedAt;
			set
			{
				this._eventInspection.CreatedAt = DateTime.Now;
			}
		}
		public DateTime? UpdatedAt
		{
			get => this._eventInspection.UpdatedAt;
			set
			{
				this.UpdatedAt = DateTime.Now;
			}
		}
		public DateTime? DeletedAt
		{
			get => this._eventInspection.DeletedAt;
			set
			{
				this.DeletedAt = DateTime.Now;
			}
		}
		#endregion
    }
}
