using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ooRuleML
{
    /*
     * ooRuleML C# Library
     *
     * @package    ooRuleML
     * @category   Library
     * @author     M. Erdem ÇORAPÇIOĞLU
     * @copyright  (c) 2006-2012
     * @license    LGPL v3
     */
    public class Oid : ICloneable
    {
        public Oid()
        {
        }

        public Oid(Oid another)
        {
            if (another.Data != null)
            {
                data = (string)another.Data.Clone();
            }

            if (another.Individual != null)
            {
                individual = (string)another.Individual.Clone();
            }
        }

        public override bool Equals(object o)
        {
            if (o == null || GetType() != o.GetType())
            {
                return false;
            }

            Oid other = new Oid((Oid)o);

            if (this.Data != null)
            {
                if (!this.Data.Equals(other.Data))
                {
                    return false;
                }
            }

            if (this.Individual != null)
            {
                if (!this.Individual.Equals(other.Individual))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int code = 1;

            if (this.Data != null)
            {
                code *= this.Data.GetHashCode();
            }

            if (this.Individual != null)
            {
                code *= this.Individual.GetHashCode();
            }

            return code;
        }

        public void Reset()
        {
            index = -1;
        }

        [XmlIgnore]
        public object Current
        {
            get
            {
                if (index == 0)
                {
                    return data;
                }
                else if (index == 1)
                {
                    return individual;
                }
                else
                {
                    return null;
                }
            }
        }

        public bool Next()
        {
            if (index < 2)
            {
                index++;
                return true;
            }
            return false;
        }

        [XmlElement(ElementName = "Data")]
        public string Data
        {
            get { return data; }
            set { data = value; }
        }

        [XmlElement(ElementName = "Ind")]
        public string Individual
        {
            get { return individual; }
            set { individual = value; }
        }

        public Object Clone()
        {
            return new Oid(this);
        }

        private string data;
        private string individual;

        [XmlIgnore]
        private int index = -1;
    }
}
