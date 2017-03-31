/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpc.shelf.tactical.task.VerifyPortType;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPort;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerType;
using br.ufc.mdcc.hpc.shelf.certify.tactical.ParTypes;
using br.ufc.mdcc.hpc.shelf.certify.ParallelCertificationSystemC4;
using br.ufc.mdcc.hpc.shelf.tactical.environment.VerifyDataPortServerTypeC4;

namespace br.ufc.mdcc.hpc.shelf.certify.ParallelCertificationSystemC4Impl 
{
	public abstract class BaseIPeer3Impl: Application, BaseIPeer3
	{
		private ITaskPort<IVerifyPortType> verify3 = null;

		protected ITaskPort<IVerifyPortType> Verify3
		{
			get
			{
				if (this.verify3 == null)
					this.verify3 = (ITaskPort<IVerifyPortType>) Services.getPort("verify3");
				return this.verify3;
			}
		}
		private IVerifyServerPort<IVerifyDataPortServerTypeC4> verify_data3 = null;

		protected IVerifyServerPort<IVerifyDataPortServerTypeC4> Verify_data3
		{
			get
			{
				if (this.verify_data3 == null)
					this.verify_data3 = (IVerifyServerPort<IVerifyDataPortServerTypeC4>) Services.getPort("verify_data3");
				return this.verify_data3;
			}
		}
		private IParTypes<IVerifyDataPortServerTypeC4> parTypes_2 = null;

		protected IParTypes<IVerifyDataPortServerTypeC4> ParTypes_2
		{
			get
			{
				if (this.parTypes_2 == null)
					this.parTypes_2 = (IParTypes<IVerifyDataPortServerTypeC4>) Services.getPort("parTypes_2");
				return this.parTypes_2;
			}
		}
	}
}