/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortType;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPort;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortClientType;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;

namespace br.ufc.mdcc.hpc.shelf.tactical.environment.impl.VerifyDataPortImpl 
{
	public abstract class BaseIVerifyClientPort: Synchronizer, 
	BaseIClientBase<IVerifyDataPortClientType>
	{
		/*private IEnvironmentPortType client_port_type = null;

		protected IEnvironmentPortType Client_port_type
		{
			get
			{
				if (this.client_port_type == null)
					this.client_port_type = (IEnvironmentPortType) Services.getPort("client_port_type");
				return this.client_port_type;
			}
		}*/


		private IVerifyDataPortClientType client = null;
		public IVerifyDataPortClientType Client { get {if (this.client == null)
				this.client = (IVerifyDataPortClientType) Services.getPort("client_port_type");
				return this.client; } }
	}
}