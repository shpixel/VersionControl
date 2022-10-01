using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMaintenance.Entities
{
    public class User
    {
        public User()
        {
            ID = Guid.NewGuid();
        }
        public Guid ID { get; set; }

        private string fullName;

        public string FullName
        {
            get 
            {
                return fullName;// string.Format("{0} {1}", LastName, FirstName); 
            }

            set
            {
                fullName = value;
            }
        }

    }
}
