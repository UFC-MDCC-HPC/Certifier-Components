/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpc.shelf.tactical.task.VerifyPortType;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPort;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerType;
using br.ufc.mdcc.hpc.shelf.certify.tactical.ISP;
using br.ufc.mdcc.hpc.shelf.certify.ParallelCertificationSystemC4;

namespace br.ufc.mdcc.hpc.shelf.certify.ParallelCertificationSystemC4Impl 
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
		private IVerifyServerPort<IVerifyDataPortServerType> verify_data2 = null;

		protected IVerifyServerPort<IVerifyDataPortServerType> Verify_data2
		{
			get
			{
				if (this.verify_data2 == null)
					this.verify_data2 = (IVerifyServerPort<IVerifyDataPortServerType>) Services.getPort("verify_data2");
				return this.verify_data2;
			}
		}
		private IISP ISP1 = null;

		protected IISP iSP1
		{
			get
			{
				if (this.ISP1 == null)
					this.ISP1 = (IISP) Services.getPort("ISP1");
				return this.ISP1;
			}
		}
	}
}