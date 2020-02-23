namespace Artnivora.Studio.Portal.Business.Services.Helpers
{
    using Artnivora.Studio.Portal.Business.Models;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

	public static class ExtensionMethods
	{
		public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users)
		{
			return users.Select(x => x.WithoutPassword());
		}

		public static User WithoutPassword(this User user)
		{
			user.Password = null;
			return user;
		}

		public static string GetDescription<T>(this T source)
		{
			var fi = source.GetType().GetField(source.ToString());
			var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

			if (attributes != null && attributes.Length > 0)
				return attributes[0].Description;

			return source.ToString();
		}
	}
}
