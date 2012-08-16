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
    public class Warden : ICloneable
    {
        public Warden()
        {
        }

        public Warden(Warden another)
        {
            Integrity refIntegrity = null;

            try
            {
                if (another.Integrity != null)
                {
                    refIntegrity = (Integrity)another.Integrity.Clone();
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }

            integrity = refIntegrity;
        }

        public override bool Equals(object o)
        {
            if (o == null || GetType() != o.GetType())
            {
                return false;
            }

            Warden other = new Warden((Warden)o);

            if (this.Integrity != null)
            {
                if (!this.Integrity.Equals(other.Integrity))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int code = 1;

            if (this.Integrity != null)
            {
                code *= this.Integrity.GetHashCode();
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
                    return integrity;
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

        [XmlElement(ElementName = "Integrity")]
        public Integrity Integrity
        {
            get { return integrity; }
            set { integrity = value; }
        }

        public Object Clone()
        {
            return new Warden(this);
        }

        private Integrity integrity;
        
        [XmlIgnore]
        private int index = -1;
    }
}
