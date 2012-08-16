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
    public class Head : ICloneable
    {
        public Head()
        {
        }

        public Head(Head another)
        {
            Atom refAtom = null;

            try
            {
                if (another.Atom != null)
                {
                    refAtom = (Atom)another.Atom.Clone();
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }

            atom = refAtom;
            atom.initialIndividuals();
        }

        public override bool Equals(object o)
        {
            if (o == null || GetType() != o.GetType())
            {
                return false;
            }

            Head other = new Head((Head)o);

            if (this.Atom != null)
            {
                if (!this.Atom.Equals(other.Atom))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int code = 1;

            if (this.Atom != null)
            {
                code *= this.Atom.GetHashCode();
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

        [XmlElement(ElementName = "Atom")]
        public Atom Atom
        {
            get { return atom; }
            set { atom = value; }
        }

        public Object Clone()
        {
            return new Head(this);
        }

        private Atom atom;

        [XmlIgnore]
        private int index = -1;
    }
}
