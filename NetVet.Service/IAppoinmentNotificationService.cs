using System;
using System.Threading.Tasks;

namespace NetVet.Service
{
    public interface IAppoinmentNotificationService
    {
        /// <summary>
        /// Sends out reminder notifications to users that have opted in for notifications
        /// to supported contact details.
        /// </summary>
        void SendReminderForUpcomingAppointments();

        /// <summary>
        /// Sends out reminder notifications to users that have opted in for notifications
        /// to supported contact details (asynchronous method).
        /// </summary>
        Task SendReminderForUpcomingAppointmentsAsync();
    }
}
