# Orchestrating the Domain Model

A command shows the intent of the user to perform an action on the system. Each command represents a single action that the user can perform on the system. The class name of commands and handlers implies intent and action.

The command handler is responsible for executing the command and updating the domain model accordingly. The command handler is also responsible for validating the command and ensuring that it is executed in a consistent manner.

The command handler is typically implemented as a method on a service class.

The design in command handlers is to have a single command handler for each command. Follow SOLID principles, especially the single responsibility principle, and the open/closed principle.

A handler is handling a single command, and it is responsible for executing the command and updating the domain model accordingly.

##Source:
https://buildplease.com/pages/fpc-10/
