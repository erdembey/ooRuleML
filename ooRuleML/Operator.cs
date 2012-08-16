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
    public class Operator : ICloneable
    {
        public Operator()
        {
        }

        public Operator(Operator another)
        {
            if (another.Relation != null)
            {
                relation = (string)another.Relation.Clone();
            }
        }

        public override bool Equals(object o)
        {
            if (o == null || GetType() != o.GetType())
            {
                return false;
            }

            Operator other = new Operator((Operator)o);

            if (this.Relation != null)
            {
                if (!this.Relation.Equals(other.Relation))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int code = 1;

            if (this.Relation != null)
            {
                code *= this.Relation.GetHashCode();
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
                    return relation;
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

        [XmlElement(ElementName = "Rel")]
        public string Relation
        {
            get
            {
                return relation;
            }
            set
            {
                relation = value;
            }           
        }

        public Object Clone()
        {
            return new Operator(this);
        }

        private string relation;

        [XmlIgnore]
        private int index = -1;
    }
}
