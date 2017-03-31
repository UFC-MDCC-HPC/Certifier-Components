/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpc.shelf.tactical.task.VerifyPortType;
using br.ufc.mdcc.hpc.shelf.certify.tactical.MCRL2;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPort;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerType;
using br.ufc.mdcc.hpc.shelf.certify.ParallelCertificationSystemSWC2;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerTypeSWC2;

namespace br.ufc.mdcc.hpc.shelf.certify.impl.ParallelCertificationSystemSWC2Impl 
{
	public abstract class BaseIPeer2Impl: Application, BaseIPeer2
	{
		private ITaskPort<IVerifyPortType> verify2 = null;

		protected ITaskPort<IVerifyPortType> Verify2
		{
			get
			{
				if (this.verify2 == null)
					this.verify2 = (ITaskPort<IVerifyPortType>) Services.getPort("verify2");
				return this.verify2;
			}
		}
		private IMCRL2<IVerifyDataPortServerTypeSWC2> mcrl2_2 = null;

		protected IMCRL2<IVerifyDataPortServerTypeSWC2> MCRL2_2
		{
			get
			{
				if (this.mcrl2_2 == null)
					this.mcrl2_2 = (IMCRL2<IVerifyDataPortServerTypeSWC2>) Services.getPort("mcrl2_2");
				return this.mcrl2_2;
			}
		}
		private IVerifyServerPort<IVerifyDataPortServerTypeSWC2> verify_data2 = null;

		protected IVerifyServerPort<IVerifyDataPortServerTypeSWC2> Verify_data2
		{
			get
			{
				if (this.verify_data2 == null)
					this.verify_data2 = (IVerifyServerPort<IVerifyDataPortServerTypeSWC2>) Services.getPort("verify_data2");
				return this.verify_data2;
			}
		}
	}
}