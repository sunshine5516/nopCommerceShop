using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain;
using Nop.Core.Data;

namespace Nop.Services
{
    public partial class Nop_demoService : Inop_demoService
    {
        private readonly IRepository<Nop_Demo> _nopDemoRepository;
        public Nop_demoService(IRepository<Nop_Demo> _nopDemoRepository)
        {
            this._nopDemoRepository = _nopDemoRepository;
        }
        public void InsertNopDemo(Nop_Demo demo)
        {
            //throw new NotImplementedException();
            if (demo == null)
                throw new ArgumentNullException("news");

            _nopDemoRepository.Insert(demo);

            //event notification
            //_eventPublisher.EntityInserted(news);
        }
    }
}
