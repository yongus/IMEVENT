using IMEVENT.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMEVENT.Services
{
    public interface IDataExtractor
    {
        void ExtractDataFromSource(string source , int idEvent);
    }
}
