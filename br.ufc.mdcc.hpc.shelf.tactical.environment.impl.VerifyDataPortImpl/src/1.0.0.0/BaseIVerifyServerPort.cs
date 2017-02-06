/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortType;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPort;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerType;

namespace br.ufc.mdcc.hpc.shelf.tactical.environment.impl.VerifyDataPortImpl 
{
	public abstract class BaseIVerifyServerPort: Synchronizer, BaseIVerifyServerPort
	<IVerifyDataPortServerType>
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
			set { server_port_type = value; }
		}*/

		private IVerifyDataPortServerType server = default(IVerifyDataPortServerType);
		public IVerifyDataPortServerType Server { set {	server = value; } get {if (this.server == null)
				this.server = (IVerifyDataPortServerType) Services.getPort("server_port_type");
				return this.server;}}
	}
}