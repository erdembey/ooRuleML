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
     * @author     M. Erdem ÇORAPÇIOÐLU
     * @copyright  (c) 2006-2012
     * @license    LGPL v3
     */
    public class Degree : ICloneable
    {
        public Degree()
        {
        }

        public Degree(Degree another)
        {
            if (another.Data != null)
            {
                data = (string)another.Data.Clone();
            }
        }

        public override bool Equals(object o)
        {
            if (o == null || GetType() != o.GetType())
            {
                return false;
            }

            Degree other = new Degree((Degree)o);

            if (this.Data != null)
            {
                if (!this.Data.Equals(other.Data))
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
                else
                {
                    return null;
                }
            }
        }

        public bool Next()
        {
            if (index < 1)
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

        public Object Clone()
        {
            return new Degree(this);
        }

        private string data;

        [XmlIgnore]
        private int index = -1;
    }
}
