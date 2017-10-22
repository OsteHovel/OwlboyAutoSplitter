
// V 1.1.0.0

state("OwlboyAutoSplitter")
{
	uint state : "OwlBoyLiveSplitLibrary.dll", 0x3020;
}

isLoading
{
	//print("My current state is: " + current.state);
	return current.state == 2;
}

start
{
	if(current.state == 1){
		return true;
	}
	return null;
}

gameTime
{
	 return null;
}

