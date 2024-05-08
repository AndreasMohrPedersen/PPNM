public class genlist<T>{
	private T[] data; //create an array "data"
	public int size => data.Length;
	public T this[int i] => data[i];
	public genlist(){data = new T[0];}
	public int add(T item){ //methods "add" used to add items to list
		T[] newdata = new T[size + 1]; //increases thesize of the array by 1
		System.Array.Copy(data, newdata, size);
		newdata[size] = item;
		data = newdata;
		return 0;
		}
}//genlist
