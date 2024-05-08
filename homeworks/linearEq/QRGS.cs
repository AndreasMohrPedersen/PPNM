using static System.Console;

public static class QRGS{
	
	public static (matrix, matrix) decomp(matrix A){//(matrix, matrix) because we want to return a tuple containing two matrices
		matrix Q = A.copy();// defines matrix Q as a copy of A
		matrix R = new  matrix(A.size1, A.size2); // creates a new matrix R with same dimensions as A
	  	
		//orthogonalize Q through GS and make R
		for(int i=0; i<A.size2;i++){
			R[i,i] =Q[i].norm(); // sets R_(i,i) equal to the norm of the i'th column in Q
			Q[i] /=R[i,i]; //calcs a_i/||a_i||
			for(int j = i+1; j<A.size2; j++){
				R[i,j] = Q[i].dot(Q[j]); //q_i(transposed)*a_j
				Q[j]-=Q[i]*R[i,j]; //a_j-<q_i|a_j>q_i
				} }
		return (Q,R);
		}//decomp
		public static vector solve(matrix Q, matrix R, vector b){ //solves eq. QRx=b => Q^TQx=Q^Tb
			/*solves eq using back substitution on R*/
			vector c = Q.T*b; //we c=Q^T*b to get eq. on form for backsubstitution
			for(int i=c.size-1;i>=0; i--){
				double sum=0;
				for(int j=i+1; j<c.size; j++){
					sum+=R[i,j]*c[j];
					}
				c[i]=(c[i]-sum)/R[i,i];
				}
		return c; //returns vector c
		}//solve
		public static double det(matrix R){//finds determinant of triangular matrix R
			/*det(A)=det(R) see linearEquatins note*/
			double determinant = 1;
			for(int i=0; i<R.size1;i++){
				determinant *= R[i,i];//product of diagonal in right triangular matrix
				}
			return determinant;
			}//det


		public static matrix inverse(matrix Q,matrix R){ // indicates the return of a matrix
			//inverse can be found using backsubstitution
			matrix inverseA = Q.T; 
			for(int j = 0; j<Q.size1; j++){//loops over rows in matrix
				vector c = inverseA[j];
				for(int i = (c.size-1);i>=0;i--){ // Back-substitution on R
					double sum=0;
					for(int k = i+1; k<c.size; k++){ 
						sum += R[i,k]*c[k];
						}
					c[i] = (c[i]- sum)/R[i,i];
						}
				inverseA[j] = c;
				}
			return inverseA;
		}//inverse

}//QRGS
