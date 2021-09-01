using DOA_Sony.DataLayer.DTOs;
using DOA_Sony.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DOA_Sony.BusinessLayer.Interfaces
{
    public interface IControlService
    {
        ControlDTO CreateControl(string name);
    }
}
