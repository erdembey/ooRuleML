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
    public class Body : ICloneable
    {
        public Body()
        {
        }

        public Body(Body another)
        {
            Atom refAtom = null;
            And refAnd = null;
            Or refOr = null;

            try
            {
                if (another.And != null)
                {
                    refAnd = (And)another.And.Clone();
                }

                if (another.Atom != null)
                {
                    refAtom = (Atom)another.Atom.Clone();
                }

                if (another.Or != null)
                {
                    refOr = (Or)another.Or.Clone();
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }

            cf = another.cf;
            or = refOr;
            and = refAnd;
            atom = refAtom;
        }

        public override bool Equals(object o)
        {
            if (o == null || GetType() != o.GetType())
            {
                return false;
            }

            Body other = new Body((Body)o);

            if (this.Atom != null)
            {
                if (!this.Atom.Equals(other.Atom))
                {
                    return false;
                }
            }

            if (this.Or != null)
            {
                if (!this.Or.Equals(other.Or))
                {
                    return false;
                }
            }

            if (this.And != null)
            {
                if (!this.And.Equals(other.And))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int code = 1;

            if (this.And != null)
            {
                code *= this.And.GetHashCode();
            }

            if (this.Atom != null)
            {
                code *= this.Atom.GetHashCode();
            }

            if (this.Or != null)
            {
                code *= this.Or.GetHashCode();
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
                    return atom;
                }
                else if (index == 1)
                {
                    return and;
                }
                else if (index == 2)
                {
                    return or;
                }
                else
                {
                    return null;
                }
            }
        }

        public bool Next()
        {
            if (index < 3)
            {
                index++;
                return true;
            }
            return false;
        }

        [XmlElement(ElementName = "Atom")]
        public Atom Atom
        {
            get { return atom; }
            set { atom = value; }
        }

        [XmlElement(ElementName = "Or")]
        public Or Or
        {
            get { return or; }
            set { or = value; }
        }

        [XmlElement(ElementName = "And")]
        public And And
        {
            get { return and; }
            set { and = value; }
        }

        public double getCF()
        {
            return CF;
        }

        [XmlIgnore]
        public double CF
        {
            set
            {
                this.cf = value;
            }
            get
            {
                return cf;
            }
        }

        public Object Clone()
        {
            return new Body(this);
        }

        private Atom atom;
        private Or or;
        private And and;

        [XmlIgnore]
        private int index = -1;
        [XmlIgnore]
        private double cf = 9;
    }
}
