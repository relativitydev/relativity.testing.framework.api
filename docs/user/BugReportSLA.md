# Bug Report SLA

This document describes the bug report resolution process, timelines, and definitions.

## Bug Report Process

1. A bug is reported using the issue tracker in either the [Relativity.Testing.Framework](https://github.com/relativitydev/relativity.testing.framework/issues/new?assignees=&labels=bug%2Ctriage&template=bug_report.yml&title=%5BBug%5D%3A+) or [Relativity.Testing.Framework.Api](https://github.com/relativitydev/relativity.testing.framework.api/issues/new?assignees=&labels=bug%2Ctriage&template=bug_report.yml&title=%5BBug%5D%3A+) repositories.
2. A maintainer will follow up on the issue asking for more information if necessary.
3. The issue will be given a label indicating the priority.
4. The maintainers of the repository will plan the work out, and complete it within the documented time frame.
5. The maintainer working on the issue will follow up when the issue is being worked on to provide information about the changes and the package that will contain the fix.

## Bug Report Severity

| Severity | Acknowledge By | Resolve By | Description | Example |
| -------- | -------------- | ---------- | ----------- | ------- |
| P1 | 1 business day | 3 business days | A defect that completely prevents Relativity.Testing.Framework from functioning. | Relativity.Testing.Framework throws an exception while ensuring the connection. |
| P2 | 5 business days | 30 business days | A defect that prevents components of Relativity.Testing.Framework from functioning on typical environments. | A service does not provide the functionality that it is supposed to provide. |
| P3 | 7 business days | 90 business days | A defect that limits usability of Relativity.Testing.Framework in less common scenarios. | A method in a service does not expose all the properties available in the API. |
| P4 | 15 business days | Varies* | A defect that can be easily worked around, or is cosmetic in nature. | All properties of a model must be passed in to prevent to ensure correct creation. |

\* We will try to address all issues in a timely manner, but depending on other priorities, it may not always be realistic to commit to a specific time frame for all less impactful bugs.