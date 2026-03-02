# SeleniumEasyTemplate

Scalable BDD automation framework template (architecture mock).

This repository is intentionally designed as a structural example of a
Selenium + ReqnRoll + NUnit automation framework.

Tests are NOT intended to run out-of-the-box. The project demonstrates
architecture and scalability only.

------------------------------------------------------------------------

## Purpose

This template showcases:

-   Clean separation of concerns
-   BDD structure (Features / StepDefinitions)
-   Page Object Model (PO)
-   Centralized locator strategy
-   Shared driver and utility layer
-   Retry configuration via ReqnRoll
-   Runsettings-based environment configuration

The goal is to present a scalable foundation that can be extended into a
production-ready automation framework.

------------------------------------------------------------------------

## Design Principles

-   Framework layer isolated from test runner
-   StepDefinitions contain no UI logic
-   Page Objects encapsulate interaction details
-   Config-driven execution model
-   Designed for CI scalability
-   Easily extendable to multi-browser & parallel execution

------------------------------------------------------------------------

## Extensibility

This skeleton can be expanded with:

-   Concrete WebDriver initialization
-   Browser management strategies
-   Environment-based base URLs
-   Parallel execution configuration
-   Reporting integrations
-   CI/CD pipelines

------------------------------------------------------------------------

## Notes

This repository represents a framework blueprint. It focuses on
architecture clarity rather than executable coverage.

------------------------------------------------------------------------

## Author

Bohdan Reshetilov
QA Automation Engineer / SDET
