using System;
using System.Collections.Generic;
using System.Text;
using wfEnginer;

namespace wfEngineTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //string sConn = "Provider=SQLOLEDB;Data Source=.\\sqlexpress;Persist Security Info=True;User ID=sa;password=qqq;Initial Catalog=assetsdb";
            string sConn ="Provider=SQLOLEDB;Data Source=192.168.0.1;Persist Security Info=True;User ID=sa;password=123456;Initial Catalog=Asset";
            wfProcessStor wfs = new wfProcessStor();
            wfs.ConnectionStr = sConn;
            //////获取流程信息
            FlowChart fc= wfs.GetProcess("1");

            Console.WriteLine("---------------------流程信息     -----------------------------------");
            Console.WriteLine("Node count:{0}", fc.Count);
            wfActivity StartNode =fc.StartNode;
            Console.WriteLine("Node Start:{0}", StartNode.Name);
            NextNode(StartNode,0);
            //Console.WriteLine("StartNode Next Count:{0}", StartNode.ChildCount);
            //Console.WriteLine("StartNode Next Node:{0}", StartNode.ChildNode(0).Node.Name);

            Console.WriteLine("Node End:{0}\n", fc.EndNode.Name);

            ///检测流程开始节点
            Console.WriteLine("Start Node ID:{0}", wfs.getStartID("1"));
            Console.WriteLine("---------------------可操作活动--------------------------------------");
            //检测流程状态
            wfCheck check = new wfCheck();
            check.ConnectionStr = sConn;
            Console.WriteLine("Task Status:{0}", check.CheckTaskStatus("1", "1"));
            //检测用户可操作流程信息
            wfActivity act = check.GetNextTask("8", "cabb3c2b-9556-4cab-b16f-d46386d13a96", "liuqj");// "admin");
            if (act != null)
            {
                Console.WriteLine("可执行活动：节点ID:{0} 节点名称:{1}", act.ID, act.Name);
            }


            Console.WriteLine("------------------Todo list-------------------------------------------");
            TodoList list = check.GetTodoList("BOBBY");
            for (int i = 0; i < list.Count; i++)
            {
                todoItem ti =list[i];
                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}",
                    list[i].TodoID, list[i].ProcessName, ti.ActID, ti.EntityID, ti.FromUser, ti.SendTime);
            }
            Console.WriteLine("--------------------执行   ------------------------------------------");
            //执行
            wfRun rr = new wfRun();
            rr.ConnectionStr = sConn;

            rr.Url = "test\\asfs";
            rr.KeyField = "F_ID";
            rr.TableName = "TEST_TABLE";
            //rr.StartProcess("BOBBY", "1", "5", "test");
            //rr.DoActivity("user1", "20", "2","2",wfConsts.RUN_STATE_ACCEPT, "test");
            //rr.DoActivity("BOBBY", "268", "8", "cb5246ac-c660-4b84-9480-9bc693c187d5", wfConsts.RUN_STATE_ACCEPT, "test df  '测试结束");
            rr.DoActivity("ruijc", "282", "6", "41d74821-f651-462a-8f17-6f70089ece4b", wfConsts.RUN_STATE_CANEL, "test df  '测试结束");
            //rr.DoActivity("liuqj", "117", "44", "cabb3c2b-9556-4cab-b16f-d46386d13a96", wfConsts.RUN_STATE_ACCEPT, "test df  '测试结束");

            Console.WriteLine("----------------------可启动项目列表--------------------------------");
            //可启动项目列表
            ProjectList pl = wfs.GetProjectList("ruijc");//admin");
            for (int i = 0; i < pl.Count; i++)
            {
                Console.WriteLine("ID:{0},Name:{1},Desc:{2}", pl[i].ID, pl[i].Name, pl[i].Desc);
            }

            Console.ReadLine();

        }

        static private void NextNode(wfActivity cNode, int level)
        {
            if (cNode.ChildCount > 0)
            {
                //Console.WriteLine("---Current Node :{0}  Next Nodes:{1}",cNode.Name, cNode.ChildCount);
                for (int i = 0; i < cNode.ChildCount; i++)
                {
                    actRule rl = cNode.ChildNode(i);
                    Console.WriteLine("{0}Node Name:{1}",new string((char)9,level+1), rl.Node.Name);
                    if (rl != null)
                    {
                        NextNode(rl.Node,level+1);
                    }

                }
            }
        }
    }
}
