﻿using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private ApplicationDbContext _db;
        public OrderHeaderRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

		public void Update(OrderHeader obj)
		{
			_db.OrderHeaders.Update(obj);
		}
		public void UpdateStatus(int id, string Orderstatus, string? paymentStatus = null)
		{
			var orderFromDb=_db.OrderHeaders.FirstOrDefault(x => x.Id == id);
			if (orderFromDb != null)
			{
				orderFromDb.OrderStatus = Orderstatus;
				if (paymentStatus != null)
				{
					orderFromDb.PaymentStatus = paymentStatus;
				}
			}
		}
		public void UpdateStripePaymentId(int id, string sessionId, string paymentItemId)
		{
			var orderFromDb = _db.OrderHeaders.FirstOrDefault(x => x.Id == id);
			orderFromDb.SessionId = sessionId;
			orderFromDb.PaymentIntentId = paymentItemId;
		}
	}
}
