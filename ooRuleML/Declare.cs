using System;
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
    public class Declare : ICloneable
    {
        public Declare()
        {
        }

        public Declare(Declare another)
        {
            if (another.Var != null)
            {
                var = (string)another.Var.Clone();
            }
        }

        public override bool Equals(object o)
        {
            if (o == null || GetType() != o.GetType())
            {
                return false;
            }

            Declare other = new Declare((Declare)o);

            if (this.Var != null)
            {
                if (!this.Var.Equals(other.Var))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int code = 1;

            if (this.Var != null)
            {
                code *= this.Var.GetHashCode();
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
                    return var;
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

        [XmlElement(ElementName = "Var")]
        public string Var
        {
            get { return var; }
            set { var = value; }
        }

        public Object Clone()
        {
            return new Declare(this);
        }

        private string var;

        [XmlIgnore]
        private int index = -1;
    }
}
