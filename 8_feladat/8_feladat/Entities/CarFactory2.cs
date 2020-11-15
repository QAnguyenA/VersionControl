using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8_feladat.Entities
{
    public class CarFactory2: Entities.IToyFactory
    {
        public Abractions.Toy CreateNew()
        {
            return new Car();
        }
    }
}
