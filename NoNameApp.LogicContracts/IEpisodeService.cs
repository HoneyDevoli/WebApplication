using System.Collections.Generic;
using NoNameApp.Entities;
using System;
using System.Data.Entity;

namespace NoNameApp.LogicContracts
{
    public interface IEpisodeService
    {
        Episode GetEpisode(int? id);
        void AddLinkEp(Episode ep);
    }
}
