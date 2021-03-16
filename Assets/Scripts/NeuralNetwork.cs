using System;
using System.Linq;
using MathNet.Numerics.Distributions;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;
using UnityEngineInternal;
using Random = UnityEngine.Random;

public class NeuralNetwork
{
	private int amountOfLayers;
	
	private Vector<float>[] biases;
	private Matrix<float>[] weights;

	private int firstLayerSize;

	public NeuralNetwork(int[] _vectorSizes)
	{
		amountOfLayers = _vectorSizes.Length;
		
		firstLayerSize = _vectorSizes[0];
		
		biases = new Vector<float>[amountOfLayers];
		weights = new Matrix<float>[amountOfLayers];
		
		for (int i = 1; i < _vectorSizes.Length; i++)
		{
			biases[i] = Vector<float>.Build.Dense(_vectorSizes[i]);
			weights[i] = Matrix<float>.Build.Dense(_vectorSizes[i], _vectorSizes[i-1]);
			
			weights[i] = randomizeMatrix(weights[i], 2f, -2f);
			biases[i] = randomizeVector(biases[i], 2f, -2f);
		}

		//Debug.Log(Predict(new [] {3f,5.2f,2.6f,4.12f}));
	}

	public NeuralNetwork(NeuralNetwork _baseNet)
	{
		amountOfLayers = _baseNet.amountOfLayers;
		biases = (Vector<float>[])_baseNet.biases.Clone();
		weights = (Matrix<float>[])_baseNet.weights.Clone();
		for (int i = 1; i < biases.Length; i++)
		{
			biases[i] = randomizeVector(biases[i], 0.1f, -0.1f);
			weights[i] = randomizeMatrix(weights[i], 0.1f, -0.1f);
		}

		firstLayerSize = _baseNet.firstLayerSize;
	}

	private static Matrix<float> randomizeMatrix(Matrix<float> _matrix, float _maxValue, float _minValue)
	{
		for (int x = 0; x < _matrix.RowCount; x++)
		{
			for (int y = 0; y < _matrix.ColumnCount; y++)
			{
				_matrix[x, y] += Random.Range(_minValue, _maxValue);
			}
		}

		return _matrix;
	}

	private static Vector<float> randomizeVector(Vector<float> _vector, float _maxValue, float _minValue)
	{
		for (int i = 0; i < _vector.Count; i++)
		{
			_vector[i] += Random.Range(_minValue, _maxValue);
		}

		return _vector;
	}
	
	
	

	public Vector<float> Predict(float[] _firstLayer)
	{
		if (_firstLayer.Length != firstLayerSize)
		{
			return null;
		}

		Vector<float> lastNeuronLayer = Vector<float>.Build.DenseOfArray(_firstLayer);
		
		for (int i = 1; i < amountOfLayers; i++)
		{
			lastNeuronLayer = activation(weights[i] * lastNeuronLayer + biases[i]);
		}

		return lastNeuronLayer;
	}

	private static Vector<float> activation(Vector<float> layer)
	{
		for (int i = 0; i < layer.Count; i++)
		{
			layer[i] = 1 / (1 + Mathf.Exp(-layer[i]));
		}

		return layer;
	}
}
