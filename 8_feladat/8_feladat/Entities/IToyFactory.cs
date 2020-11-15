using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _8_feladat.Abractions;

namespace _8_feladat.Entities
{
    public class IToyFactory : Abractions.IToyFactory
    {
        public Abractions.Toy CreateNew()
        {
            return new Toy();
        }
    }
}
