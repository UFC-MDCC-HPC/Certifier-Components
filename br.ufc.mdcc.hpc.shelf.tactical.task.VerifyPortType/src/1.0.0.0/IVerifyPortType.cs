using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.task.TaskPortType;

namespace br.ufc.mdcc.hpc.shelf.tactical.task.VerifyPortType
{
	public interface IVerifyPortType : BaseIVerifyPortType, ITaskPortType
	{
	   
	}
	public class IVerify{
	
	    public static object VERIFY_PERFORM = new Object();
	    public static object VERIFY_CONCLUSIVE = new Object();
	    public static object VERIFY_INCONCLUSIVE = new Object();
	
	}
}