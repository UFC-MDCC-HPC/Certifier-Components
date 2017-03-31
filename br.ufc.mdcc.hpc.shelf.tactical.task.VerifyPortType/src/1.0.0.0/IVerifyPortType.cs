using br.ufc.pargo.hpe.kinds;
using System;
using br.ufc.mdcc.hpc.storm.binding.task.TaskPortType;

namespace br.ufc.mdcc.hpc.shelf.tactical.task.VerifyPortType
{
	public interface IVerifyPortType : BaseIVerifyPortType, ITaskPortType
	{
	   
	}
	public class IVerify{
	
		public static object VERIFY_PERFORM = "VERIFY_PERFORM".GetHashCode();
		public static object VERIFY_CONCLUSIVE = "VERIFY_CONCLUSIVE".GetHashCode();
		public static object VERIFY_INCONCLUSIVE = "VERIFY_INCONCLUSIVE".GetHashCode();
	
	}
}