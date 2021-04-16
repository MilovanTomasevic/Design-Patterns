package com.activemesa.behavioral.strategy.exercise;

import org.junit.Test;
import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertTrue;

public class Evaluate
{
  @Test
  public void positiveTestOrdinaryStrategy()
  {
    DiscriminantStrategy strategy = new OrdinaryDiscriminantStrategy();
    QuadraticEquationSolver solver = new QuadraticEquationSolver(strategy);
    Pair<Complex, Complex> results = solver.solve(1, 10, 16);
    assertEquals(new Complex(-2, 0), results.first);
    assertEquals(new Complex(-8, 0), results.second);
  }

  @Test
  public void positiveTestRealStrategy()
  {
    DiscriminantStrategy strategy = new RealDiscriminantStrategy();
    QuadraticEquationSolver solver = new QuadraticEquationSolver(strategy);
    Pair<Complex, Complex> results = solver.solve(1, 10, 16);
    assertEquals(new Complex(-2, 0), results.first);
    assertEquals(new Complex(-8, 0), results.second);
  }

  @Test
  public void negativeTestOrdinaryStrategy()
  {
    OrdinaryDiscriminantStrategy strategy = new OrdinaryDiscriminantStrategy();
    QuadraticEquationSolver solver = new QuadraticEquationSolver(strategy);
    Pair<Complex, Complex> results = solver.solve(1, 4, 5);
    assertEquals(new Complex(-2, 1), results.first);
    assertEquals(new Complex(-2, -1), results.second);
  }

  @Test
  public void negativeTestRealStrategy()
  {
    DiscriminantStrategy strategy = new RealDiscriminantStrategy();
    QuadraticEquationSolver solver = new QuadraticEquationSolver(strategy);
    Pair<Complex, Complex> results = solver.solve(1, 4, 5);

    assertTrue(results.first.isNaN());
    assertTrue(results.second.isNaN());
  }
}
