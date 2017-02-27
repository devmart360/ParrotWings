using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Devmart360.ParrotWings.Users;

namespace Devmart360.ParrotWings.Models
{
    /// <summary>
    /// Операция перевода внутренних денег
    /// </summary>
    [Table("Transactions")]
    public class Transaction : AuditedEntity<long>, IHasCreationTime
    {
        // следующие поля наследуются от AuditedEntity и поддерживаются автоматически ABP
        // CreationTime, CreatorUserId
        // LastModificationTime, LastModifierUserId

        /// <summary>
        /// Сумма перевода
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// Тип перевода (+1,-1)
        /// </summary>
        public int Type { get; set; }       // todo: сделать enum

        /// <summary>
        /// Отправитель
        /// </summary>
        public User CreatorUser { get; set; }

        /// <summary>
        /// Получатель
        /// </summary>
        public User RecipientUser { get; set; }
    }
}
