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
    public class Implies : ICloneable
	{
        public Implies()
        {
        }

        public Implies(Implies another)
        {
            Oid refOid = null;
            Head refHead = null;
            Body refBody = null;

            try
            {
                if (another.Oid != null)
                {
                    refOid = (Oid)another.Oid.Clone();
                }

                if (another.Body != null)
                {
                    refBody = (Body)another.Body.Clone();
                }

                if (another.Head != null)
                {
                    refHead = (Head)another.Head.Clone();
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }

            oid = refOid;
            body = refBody;
            head = refHead;
        }

        public override bool Equals(object o)
        {
            if (o == null || GetType() != o.GetType())
            {
                return false;
            }

            Implies other = new Implies((Implies)o);

            if (this.oid != null)
            {
                if (!this.Oid.Equals(other.Oid))
                {
                    return false;
                }
            }

            if (this.Head != null)
            {
                if (!this.Head.Equals(other.Head))
                {
                    return false;
                }
            }

            if (this.Body != null)
            {
                if (!this.Body.Equals(other.Body))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int code = 1;

            if (this.Oid != null)
            {
                code *= this.Oid.GetHashCode();
            }

            if (this.Head != null)
            {
                code *= this.Head.GetHashCode();
            }

            if (this.Body != null)
            {
                code *= this.Body.GetHashCode();
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
                    return oid;
                }
                else if (index == 1)
                {
                    return head;
                }
                else if (index == 2)
                {
                    return body;
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

        [XmlElement(ElementName = "head")]
        public Head Head
        {
            set
            {
                head = value;
            }
            get
            {
                return head;
            }
        }

        [XmlElement(ElementName = "body")]
        public Body Body
        {
            set
            {
                body = value;
            }
            get
            {
                return body;
            }
        }

        [XmlElement(ElementName = "oid")]
        public Oid Oid
        {
            get { return oid; }
            set { oid = value; }
        }

        public Object Clone()
        {
            return new Implies(this);
        }

        private Oid oid;
        private Head head;
        private Body body;

        [XmlIgnore]
        private int index = -1;
	}
}
