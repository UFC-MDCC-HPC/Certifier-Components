/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortType;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPort;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortClientTypeSWC2;

namespace br.ufc.mdcc.hpc.shelf.tactical.environment.impl.VerifyDataPortSWC2Impl 
{
	public abstract class BaseIVerifyClientPortSWC2: Synchronizer, 
	BaseIVerifyClientPort<IVerifyDataPortClientTypeSWC2>
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

		private IVerifyDataPortClientTypeSWC2 client = null;
		public IVerifyDataPortClientTypeSWC2 Client { get {if (this.client == null)
				this.client = (IVerifyDataPortClientTypeSWC2) Services.getPort("client_port_type");
				return this.client; } }
	}
}