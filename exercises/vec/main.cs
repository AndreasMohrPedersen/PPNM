using static System.Console;
class main{
	static void Main(){
		vec u = new vec(1,2,3);
		vec v = new vec(4,5,6);
		vec w = u;
		int a = 2;
		u.print("u:");
		v.print("v:");
		w.print("w:");
		WriteLine($"a*u= {a*u}");
		WriteLine($"u*a= {u*a}");
		WriteLine($"u+v= {u+v}");
		WriteLine($"-u= {-u}");
		WriteLine($"u-v= {u-v}");
		WriteLine($"dot(u, v)= {vec.dot(u, v)}");
		WriteLine($"u.dot(v)= {u.dot(v)}");
		WriteLine($"cross(u,v)= {vec.cross(u,v)}");
		WriteLine($"u.cross(v)= {u.cross(v)}");
		WriteLine($"u.norm= {u.norm()}");
		WriteLine($"vec.norm(u)= {vec.norm(u)}");
		WriteLine($"u.approx(v)= {u.approx(v)}");
		WriteLine($"u.approx(w)= {u.approx(w)}");
		WriteLine($"vec.approx(u,v)= {vec.approx(u,v)}");
		WriteLine($"vec.approx(u,w)= {vec.approx(u,w)}");
	}//Main
}//main
