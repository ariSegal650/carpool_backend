using LogicService.Data;
using LogicService.EO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicService.Services
{
    public class RequstService
    {
        private readonly DataContexst _DataContexst;
        public RequstService(DataContexst dataContexst)
        {
            _DataContexst = dataContexst;
        }
        public async Task<Boolean> AddReuqst(Request request)
        {
            try
            {
                await _DataContexst._requsts.InsertOneAsync(request);
                return true;
            }
            catch (Exception)
            {
                return false;
                //throw;
            }

        }
    }
}
