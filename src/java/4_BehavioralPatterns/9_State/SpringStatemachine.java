package com.activemesa.behavioral.state.spring;

import org.springframework.statemachine.StateMachine;
import org.springframework.statemachine.config.StateMachineBuilder;
import org.springframework.statemachine.state.State;
import org.springframework.statemachine.transition.Transition;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.util.Collection;
import java.util.EnumSet;
import java.util.List;
import java.util.stream.Collectors;

// plural because SSM defines a class called State already
enum States
{
  OFF_HOOK, // starting
  ON_HOOK, // terminal
  CONNECTING,
  CONNECTED,
  ON_HOLD
}

enum Events
{
  CALL_DIALED,
  HUNG_UP,
  CALL_CONNECTED,
  PLACED_ON_HOLD,
  TAKEN_OFF_HOLD,
  LEFT_MESSAGE,
  STOP_USING_PHONE
}

class SpringStatemachineDemo
{
  public static StateMachine<States, Events> buildMachine()
    throws Exception
  {
    StateMachineBuilder.Builder<States, Events> builder
      = StateMachineBuilder.builder();

    builder.configureStates()
      .withStates()
        .initial(States.OFF_HOOK)
        .states(EnumSet.allOf(States.class));

    builder.configureTransitions()
      .withExternal()
        .source(States.OFF_HOOK)
        .event(Events.CALL_DIALED)
        .target(States.CONNECTING)
        .and()
      .withExternal()
        .source(States.OFF_HOOK)
        .event(Events.STOP_USING_PHONE)
        .target(States.ON_HOOK)
        .and()
      .withExternal()
        .source(States.CONNECTING)
        .event(Events.HUNG_UP)
        .target(States.OFF_HOOK)
        .and()
      .withExternal()
        .source(States.CONNECTING)
        .event(Events.CALL_CONNECTED)
        .target(States.CONNECTED)
        .and()
      .withExternal()
        .source(States.CONNECTED)
        .event(Events.LEFT_MESSAGE)
        .target(States.OFF_HOOK)
        .and()
      .withExternal()
        .source(States.CONNECTED)
        .event(Events.HUNG_UP)
        .target(States.OFF_HOOK)
        .and()
      .withExternal()
        .source(States.CONNECTED)
        .event(Events.PLACED_ON_HOLD)
        .target(States.OFF_HOOK)
        .and()
      .withExternal()
        .source(States.ON_HOLD)
        .event(Events.TAKEN_OFF_HOLD)
        .target(States.CONNECTED)
        .and()
      .withExternal()
        .source(States.ON_HOLD)
        .event(Events.HUNG_UP)
        .target(States.OFF_HOOK);

    return builder.build();
  }

  // requires org.springframework.statemachine
  public static void main(String[] args) throws Exception
  {
    StateMachine<States, Events> machine = buildMachine();
    machine.start();

    States exitState = States.ON_HOOK;

    BufferedReader console = new BufferedReader(
      new InputStreamReader(System.in)
    );

    while (true)
    {
      State<States, Events> state = machine.getState();

      System.out.println("The phone is currently " + state.getId());
      System.out.println("Select a trigger:");

      List<Transition<States, Events>> ts = machine.getTransitions()
        .stream()
        .filter(t -> t.getSource() == state)
        .collect(Collectors.toList());
      for (int i = 0; i < ts.size(); ++i)
      {
        System.out.println("" + i + ". " +
          ts.get(i).getTrigger().getEvent());
      }

      boolean parseOK;
      int choice = 0;
      do
      {
        try
        {
          System.out.println("Please enter your choice:");

          choice = Integer.parseInt(console.readLine());
          parseOK = choice >= 0 && choice < ts.size();
        }
        catch (Exception e)
        {
          parseOK = false;
        }
      } while (!parseOK);

      // perform the transition
      machine.sendEvent(ts.get(choice).getTrigger().getEvent());

      if (machine.getState().getId() == exitState)
        break;
    }
    System.out.println("And we are done!");
  }
}
