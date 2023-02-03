using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZwSoft.ZwCAD.DatabaseServices;

namespace DrillingHoles
{
    class DWG_HolesRepository : HolesRepository
    {
        private ObjectIdCollection _items;
        private Transaction _tr;

        public DWG_HolesRepository(ObjectIdCollection items, Transaction tr)
        {
            _tr = tr;
            _items = items;
            List<ZWHole> zwholes = new ZWHolesFactory(_items, _tr).AsZWHoles();
            zwholes.ForEach( h =>
                {
                    this.Holes.Add(ZWHoleToHole.Convert(h, _tr));
            });
        }

        public override void Save()
        {
            throw new NotImplementedException();
        }
    }
}
