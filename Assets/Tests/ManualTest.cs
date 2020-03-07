using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ManualTest
    {
        [Test]
        public void ManualModeHappyPath()
        {
            ManualChecklist checklist = new ManualChecklist();
            ManualChecklistValidator validator = new ManualChecklistValidator(checklist);

            validator.onFail += () => Assert.Fail("failed");
            validator.onError += err => Assert.Fail(err);
            validator.onPass += (TaskType t, bool compl) => checklist.SetTaskCompletion(t, compl);
            validator.onSuccess += () => Assert.Pass("passed");

            validator.Validate(TaskType.NoSafetyMat, true);
            validator.Validate(TaskType.NoLightRays, true);
            validator.Validate(TaskType.DoorClosed, true);
            validator.Validate(TaskType.CycleHoldPressed, true);
            validator.Validate(TaskType.InterruptorManual, true);
            validator.Validate(TaskType.RobotT1, true);
            validator.Validate(TaskType.RobotFixed, true);
            validator.Validate(TaskType.InterruptorAutomatic, true);
            validator.Validate(TaskType.ResetPressed, true);
            Assert.Fail("No Event Triggered.");
        }


        [Test]
        public void PressingCycleHoldWithDoorOpenCausesError()
        {
            ManualChecklist checklist = new ManualChecklist();
            ManualChecklistValidator validator = new ManualChecklistValidator(checklist);

            validator.onError += err => Assert.Pass(err);
            validator.onPass += (TaskType t, bool compl) => checklist.SetTaskCompletion(t, compl);
            validator.onSuccess += () => Assert.Fail("passed");

            validator.Validate(TaskType.NoSafetyMat, true);
            validator.Validate(TaskType.NoLightRays, true);
            validator.Validate(TaskType.CycleHoldPressed, true);
            Assert.Fail("No Event Triggered.");
        }

        [Test]
        public void PressingInterruptorAutomaticCausesError()
        {
            ManualChecklist checklist = new ManualChecklist();
            ManualChecklistValidator validator = new ManualChecklistValidator(checklist);

            validator.onError += err => Assert.Pass(err);
            validator.onPass += (TaskType t, bool compl) => checklist.SetTaskCompletion(t, compl);
            validator.onSuccess += () => Assert.Fail("passed");

            validator.Validate(TaskType.NoSafetyMat, true);
            validator.Validate(TaskType.NoLightRays, true);
            validator.Validate(TaskType.InterruptorAutomatic, true);
            Assert.Fail("reached end");
        }

        [Test]
        public void PressingCycleStartCausesError()
        {
            var validator = GetFailValidator();
            validator.Validate(TaskType.NoSafetyMat, true);
            validator.Validate(TaskType.NoLightRays, true);
            validator.Validate(TaskType.CycleStartPressed, true);
            Assert.Fail("reached end");
        }

        [Test]
        public void PressingEmergencyCausesError()
        {
            var validator = GetFailValidator();
            validator.Validate(TaskType.NoSafetyMat, true);
            validator.Validate(TaskType.NoLightRays, true);
            validator.Validate(TaskType.EmergencyPressed, true);
            Assert.Fail("reached end");
        }

        [Test]
        public void PressingT1BeforeCycleHoldCausesError()
        {
            var validator = GetFailValidator();
            validator.Validate(TaskType.NoSafetyMat, true);
            validator.Validate(TaskType.NoLightRays, true);
            validator.Validate(TaskType.DoorClosed, true);
            validator.Validate(TaskType.RobotT1, true);
            Assert.Fail("reached end");
        }

        [Test]
        public void PressingT1WithDoorOpenCausesError()
        {
            var validator = GetFailValidator();
            validator.Validate(TaskType.NoSafetyMat, true);
            validator.Validate(TaskType.NoLightRays, true);
            validator.Validate(TaskType.RobotT1, true);
            Assert.Fail("reached end");
        }

        [Test]
        public void PressingT1WithoutInterruptorManualCausesError()
        {
            var validator = GetFailValidator();
            validator.Validate(TaskType.NoSafetyMat, true);
            validator.Validate(TaskType.NoLightRays, true);
            validator.Validate(TaskType.DoorClosed, true);
            validator.Validate(TaskType.CycleHoldPressed, true);
            validator.Validate(TaskType.RobotT1, true);
            Assert.Fail("reached end");
        }

        [Test]
        public void FixingRobotWithoutT1CausesFail()
        {
            var validator = GetFailValidator();
            validator.Validate(TaskType.NoSafetyMat, true);
            validator.Validate(TaskType.NoLightRays, true);
            validator.Validate(TaskType.DoorClosed, true);
            validator.Validate(TaskType.RobotFixed, true);
            Assert.Fail("reached end");
        }

        [Test]
        public void PressingResetAfterFixWithoutAutomaticCausesFail()
        {
            var validator = GetFailValidator();
            validator.Validate(TaskType.NoSafetyMat, true);
            validator.Validate(TaskType.NoLightRays, true);
            validator.Validate(TaskType.DoorClosed, true);
            validator.Validate(TaskType.CycleHoldPressed, true);
            validator.Validate(TaskType.InterruptorManual, true);
            validator.Validate(TaskType.RobotT1, true);
            validator.Validate(TaskType.RobotFixed, true);
            validator.Validate(TaskType.ResetPressed, true);
            Assert.Fail("reached end");
        }

        [Test]
        public void SettingInterruptorAutomaticWithoutFixCausesFail()
        {
            var validator = GetFailValidator();
            validator.Validate(TaskType.NoSafetyMat, true);
            validator.Validate(TaskType.NoLightRays, true);
            validator.Validate(TaskType.DoorClosed, true);
            validator.Validate(TaskType.CycleHoldPressed, true);
            validator.Validate(TaskType.InterruptorManual, true);
            validator.Validate(TaskType.RobotT1, true);
            validator.Validate(TaskType.InterruptorAutomatic, true);
            Assert.Fail("reached end");

        }


        ChecklistValidator GetFailValidator()
        {
            ManualChecklist checklist = new ManualChecklist();
            ManualChecklistValidator validator = new ManualChecklistValidator(checklist);
            validator.onError += err => Debug.Log(err);
            validator.onFail += () => Assert.Pass();
            validator.onPass += (TaskType t, bool compl) => checklist.SetTaskCompletion(t, compl);
            validator.onSuccess += () => Assert.Fail("passed");
            return validator;
        }
    }
}
