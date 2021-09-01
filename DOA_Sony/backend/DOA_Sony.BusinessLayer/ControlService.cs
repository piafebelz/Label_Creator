using DOA_Sony.BusinessLayer.Interfaces;
using DOA_Sony.DataLayer;
using DOA_Sony.DataLayer.DTOs;
using DOA_Sony.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DOA_Sony.BusinessLayer
{
    public class ControlService : IControlService
    {
        private SonyServiceContext _db;
        public ControlService(SonyServiceContext db)
        {
            _db = db;
        }
        public ControlDTO CreateControl(string name)
        {
            var control = new Control
            {
                ControlName = name,
                CDATE = DateTime.UtcNow
            };
            _db.Control.Add(control);
            _db.SaveChanges();
            return control.ToDTO();
        }
    }
}
