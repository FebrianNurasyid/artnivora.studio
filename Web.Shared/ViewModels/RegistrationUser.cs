using Artnivora.Studio.Portal.Business.Models;

namespace Artnivora.Studio.Portal.Web.Shared.ViewModels
{
	public class RegistrationUser : User
	{
		private int _targetAudienceReference;

		private int _holidayWeekReference;

		public int TargetAudienceReference
		{
			get
			{
				return this._targetAudienceReference;
			}

			set
			{
				this._targetAudienceReference = value;
			}
		}

		public int HolidayWeekReference
		{
			get
			{
				return this._holidayWeekReference;
			}

			set
			{
				this._holidayWeekReference = value;
			}
		}
	}
}
