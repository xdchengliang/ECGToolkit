/***************************************************************************
Copyright 2008, Thoraxcentrum, Erasmus MC, Rotterdam, The Netherlands

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

	http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

Written by Maarten JB van Ettinger.

****************************************************************************/
using System;
using System.Collections;
using System.Xml;

namespace ECGConversion.aECG
{
	public sealed class aECGAssignedAuthorType : aECGElement
	{
		public aECGDevice AssignedDevice = null;
		public aECGPerson AssignedPerson = null;

		public aECGAssignedAuthorType() : base("assignedAuthorType")
		{
		}

		public override int Read(System.Xml.XmlReader reader)
		{
			while (reader.Read())
			{
				if ((reader.NodeType == XmlNodeType.Comment)
				||  (reader.NodeType == XmlNodeType.Whitespace))
					continue;

				if ((string.Compare(reader.Name, Name) == 0)					
				&&  (reader.NodeType == XmlNodeType.EndElement))
					return 0;

				int ret = 0;

				if (string.Compare(reader.Name, "assignedDevice") == 0)
				{
					AssignedDevice = new aECGDevice("assignedDevice");

					ret = AssignedDevice.Read(reader);
				}
				else if (string.Compare(reader.Name, "assignedPerson") == 0)
				{
					AssignedPerson = new aECGPerson("assignedPerson");

					ret = AssignedPerson.Read(reader);
				}

				if (ret != 0)
					return ret; 
			}

			return -1;
		}

		public override int Write(XmlWriter writer)
		{
			if (!Works())
				return 0;

			writer.WriteStartElement(Name);

			aECGElement.WriteAll(this, writer);

			writer.WriteEndElement();

			return 0;
		}

		public override bool Works()
		{
			if (AssignedDevice != null)
				return AssignedDevice.Works();
			else if (AssignedPerson != null)
				return AssignedPerson.Works();

			return false;
		}

		public void Set(aECGAssignedAuthorType aat)
		{
			if ((aat.AssignedDevice != null)
			&&	(aat.AssignedPerson == null))
			{
				this.AssignedDevice = new aECGDevice("assignedDevice");

				this.AssignedDevice.Set(aat.AssignedDevice);
			}
			else if ((aat.AssignedDevice == null)
				&&	(aat.AssignedPerson != null))
			{
				this.AssignedPerson = new aECGPerson("assignedPerson");

				this.AssignedPerson.Set(aat.AssignedPerson);
			}
		}
	}
}
