using AspNetCoreHero.ToastNotification.Abstractions;
using CRM.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace CRM.UI.Controllers
{
    public class BaseController : Controller
    {
        public static string ToTitleCase(string str)
        {
            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
            return myTI.ToTitleCase(str);
        }

        //public void Notify(NotificationType notificationType, string Message)
        //{
        //    INotyfService _notifyService;
        //    if (notificationType == NotificationType.error)
        //    {
        //        _notifyService.Error(Message);
        //    }
        //    if (notificationType == NotificationType.warning)
        //    {
        //        _notifyService.Warning(Message);
        //    }
        //    if (notificationType == NotificationType.success)
        //    {
        //        _notifyService.Success(Message);
        //    }
        //}
    }
}