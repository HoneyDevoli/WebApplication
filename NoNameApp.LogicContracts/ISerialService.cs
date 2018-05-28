using System.Collections.Generic;
using NoNameApp.Entities;
using System;
using NoNameApp.BLL.Infrastructure;

namespace NoNameApp.LogicContracts
{
    public interface ISerialService
    {
        Serial GetSerial(int? id);
        IEnumerable<Serial> GetSerials();
        List<Serial> GetSearchSerials(string name);
        OperationDetails Adding(string Name, DateTime date, string pic, string country, string desc, int dur, bool status);
        OperationDetails AddSeason(int id, int count);
        OperationDetails AddEpisode(int id, int count, string name, DateTime? date);
        OperationDetails Delete(int id);
        OperationDetails DeleteSeason(int id);
        OperationDetails DeleteEpisode(int id);
        OperationDetails ChangeEpisode(int id, string name, int number);
        OperationDetails ChangeDescription(int id, string descrtiption);
        void Dispose();
        OperationDetails ChangeSeason(int id, int number);
        OperationDetails ChangeInfoSer(int id, DateTime? begin, string linkPic, string country, int duration, bool status);
        OperationDetails ChangeSerialName(int id, string name);
      
    }
}
