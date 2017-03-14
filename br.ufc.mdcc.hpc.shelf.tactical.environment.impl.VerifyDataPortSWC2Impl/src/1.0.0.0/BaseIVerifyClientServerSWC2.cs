/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortType;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPort;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerTypeSWC2;
namespace br.ufc.mdcc.hpc.shelf.tactical.environment.impl.VerifyDataPortSWC2Impl 
{
	public abstract class BaseIVerifyClientServerSWC2: Synchronizer, BaseIVerifyServerPort<IVerifyDataPortServerTypeSWC2>
	{
		/*private IEnvironmentPortType server_port_type = null;

		protected IEnvironmentPortType Server_port_type
		{
			get
			{
				if (this.server_port_type == null)
					this.server_port_type = (IEnvironmentPortType) Services.getPort("server_port_type");
				return this.server_port_type;
			}
		}*/
		private IVerifyDataPortServerTypeSWC2 server = default(IVerifyDataPortServerTypeSWC2);
		public IVerifyDataPortServerTypeSWC2 Server { set {	server = value; } get {if (this.server == null)
				this.server = (IVerifyDataPortServerTypeSWC2) Services.getPort("server_port_type");
				return this.server;}}
	}
}