# Sino.NLog.Redis

### 简介
----
该库是为了解决Nlog无法使用TCP与Logstash通信，但是UDP无法满足需求后，通过查阅相关的开源项目从而自己扩展扩出来的一个简单的库，该库采用`list`的方式保存到Redis，然后logstash通过Redis读取日志信息并保存到`elasticsearch`。

### 如何使用
----

其他操作同`NLog`，需要打开nlog.config并加入以下配置：
```
  <extensions>
    <add assembly="Sino.NLog.Redis" />
  </extensions>
```
该配置表示将该扩展插件加载，然后需要增加对应的`target`：
```
    <target xsi:type="redis" name="redis" includeAllProperties="true" host="127.0.0.1" port="6379" redisKey="logstash" >
      <layout xsi:type="JsonLayout">
        <attribute name="type" layout="tms" />
        <attribute name="date" layout="${longdate}" />
        <attribute name="level" layout="${level:uppercase=true}" />
        <attribute name="callSite" layout="${callsite:className=true:methodName=true:skipFrames=1}" />
        <attribute name="message" layout="${message}" />
        <attribute name="exception" layout="${exception:format=toString,Data}" />
        <attribute name="fileName" layout="${callsite:fileName=true:includeSourcePath=true}" />
      </layout>
    </target>
```
其中每个配置说明如下：
- Host（必填）：为Redis的IP地址
- Port（必填）：为Redis的端口
- Password（选填）：为Redis的密钥
- RedisKey（必填）：为Redis中保存的Key名称

对应`logstash`的配置如下所示：
```
redis{ data_type => "list" key => "logstash" host => "127.0.0.1" port => 6379 codec => "json" }
```