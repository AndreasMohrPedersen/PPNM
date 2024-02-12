using static System.Console;
using static System.Math;
public class vec{
	public double x, y, z;

	//constructors
	public vec(){ x=0; y=0; z=0; }
	public vec(double a,double b,double c){x=a; y=b; z=c; }

	//operators
	public static vec operator*(vec v, double c){return new vec(c*v.x,c*v.y,c*v.z);}
	public static vec operator*(double c, vec v){return v*c;}
	public static vec operator+(vec u, vec v){return new vec(u.x+v.x, u.y+v.y, u.z+v.z);}
	public static vec operator-(vec u){return new vec(-u.x,-u.y,-u.z);}
	public static vec operator-(vec u, vec v){return new vec(u.x-v.x, u.y-v.y, u.z-v.z);}

	//methods
	public void print(string s){Write(s);WriteLine($"{x} {y} {z}");}
	public void print(){this.print("");} //fx u.print prints "x", "y", "z"

	//dot product
	public double dot(vec other){return this.x*other.x+this.y*other.y+this.z*other.z;}
	public static double dot(vec v,vec w){return v.x*w.x+v.y*w.y+v.z*w.z;}
	//cross product
	public vec cross(vec other){
		return new vec(this.y*other.z-this.z*other.y, this.z*other.x-this.x*other.z, this.x*other.y-this.y*other.x);
		}//u.cross(v)
	public static vec cross(vec v, vec w){
		return new vec(v.y*w.z-v.z*w.y, v.z*w.x-v.x*w.z, v.x*w.y-v.y*w.x);
		}//vec.cross(u,v)
	//norm
	public double norm(){return Sqrt(this.x*this.x+this.y*this.y+this.z*this.z);}//v.norm()
	public static double norm(vec v){return Sqrt(v.x*v.x+v.y*v.y+v.z*v.z);}//vec.norm(v)
	//approx method
	static bool approx(double a,double b,double acc=1e-9,double eps=1e-9){
		if(Abs(a-b)<acc)return true;
		if(Abs(a-b)<(Abs(a)+Abs(b))*eps)return true;
		return false;
		}
	public bool approx(vec other){
		if(!approx(this.x,other.x)) return false;
		if(!approx(this.y,other.y)) return false;
		if(!approx(this.z,other.z)) return false;
		return true;
		}
	public static bool approx(vec u, vec v){return u.approx(v);}
	
	public override string ToString(){ return $"{x} {y} {z}";}
}//main
