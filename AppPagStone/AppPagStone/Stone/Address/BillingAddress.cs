using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPagStone.Stone.Address
{
    public class BillingAddress
    {
        /// <summary>
        /// País. Opções: Brazil, USA, Argentina, Bolivia, Chile, Colombia, Uruguay, Mexico, Paraguay
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Cidade
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Distrito
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// Logradouro
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Número
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Complemento
        /// </summary>
        public string Complement { get; set; }

        /// <summary>
        /// CEP
        /// </summary>
        public string ZipCode { get; set; }
    }
}
