using System;
using Abp.Notifications;

namespace Devmart360.ParrotWings.Notifications
{
    [Serializable]
    public class BalanceChangedNotificationData : NotificationData
    {
        public double Balance { get; set; }

        public BalanceChangedNotificationData(double balance)
        {
            Balance = balance;
        }
    }
}