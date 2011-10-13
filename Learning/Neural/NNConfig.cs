namespace AIMA.Core.Learning.Neural
{
    using System;
    using System.Collections.Generic;
/*
 * a holder for config data for neural networks and possibly for other
 * learning systems.
 */

/**
 * @author Ravi Mohan
 * 
 */
public class NNConfig {
	private Dictionary<String, Object> hash;

	public NNConfig(Dictionary<String, Object> hash) {
		this.hash = hash;
	}

	public NNConfig() {
		this.hash = new Dictionary<String, Object>();
	}

	public double getParameterAsDouble(String key) {

		return (Double) hash[key];
	}

	public int getParameterAsint(String key) {

		return (int) hash[key];
	}

	public void setConfig(String key, Double value) {
		hash.Add(key, value);
	}

	public void setConfig(String key, int value) {
		hash.Add(key, value);
	}
}
}