public class butchertables
{
	public static (matrix, vector, vector, vector) rk45table()
	{
		matrix a = new matrix($"0 0 0 0 0 0;
									{1f/4} 0 0 0 0 0;
									{3f/32} {9f/32} 0 0 0 0;
									{1932f/2197} {-7200f/2197} {7296f/2197} 0 0 0;
									{439f/216} -8 {3680f/513} {-845f/4104} 0 0;
									{-8f/27} 2 {-3544f/2565} {1859f/4104} {-11f/40} 0"); //coefficients for k's
		vector stepsizes = new vector($"0 {1f/4} {3f/8} {12f/13} 1 {1f/2}");
		vector b5 = new vector($"{16f/135} 0 {6656f/12825} {28561f/56430} {-9f/50} {2f/55}");//5'th order solution
		vector b4 = new vector($"{25f/216} 0 {1408f/2565} {2197f/4104} {-1f/5} 0");//lowerorder solution(4'th order)
		return (a, b5, b4, stepsizes);
	}
}//butchertableau
